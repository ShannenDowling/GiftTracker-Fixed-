using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;

namespace TodoAWSSimpleDB
{
	public class SimpleDBStorage : ISimpleDBStorage
	{
        AmazonSimpleDBClient client;
        string tableName = "Gifts";

        public List<TodoItem> Items { get; private set; }

        public SimpleDBStorage()
        {
            var credentials = new CognitoAWSCredentials(
                                  Constants.CognitoIdentityPoolId,
                                  RegionEndpoint.EUWest1);
            var config = new AmazonSimpleDBConfig();
            config.RegionEndpoint = RegionEndpoint.EUWest1;
            client = new AmazonSimpleDBClient(credentials, config);

            Items = new List<TodoItem>();
            SetupDomain();
        }

        async Task SetupDomain()
        {
            var domainExists = await IsExistingDomain();
            if (!domainExists)
            {
                await CreateDomain();
            }
        }

        async Task<bool> IsExistingDomain()
        {
            try
            {
                var response = await client.ListDomainsAsync(new ListDomainsRequest());
                foreach (var domain in response.DomainNames)
                {
                    if (domain == tableName)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return false;
        }

        async Task CreateDomain()
        {
            try
            {
                await client.CreateDomainAsync(new CreateDomainRequest { DomainName = tableName });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        List<Amazon.SimpleDB.Model.Attribute> ToSimpleDBAttributes(TodoItem item)
        {
            return new List<Amazon.SimpleDB.Model.Attribute>() {
                new Amazon.SimpleDB.Model.Attribute () {
                    Name = "Name",
                    Value = item.Name
                },
                new Amazon.SimpleDB.Model.Attribute () {
                    Name = "Link",
                    Value = item.Link
                },
                new Amazon.SimpleDB.Model.Attribute () {
                    Name = "Price",
                    Value = item.Price.ToString()
                },
                new Amazon.SimpleDB.Model.Attribute () {
                    Name = "Bought",
                    Value = item.Bought.ToString ()
                }//,
				// The users email address is used to identify data in SimpleDB
				//new Amazon.SimpleDB.Model.Attribute () {
					//Name = "User",
					//Value = App.User.Email
				//}
			};
        }

        List<ReplaceableAttribute> ToSimpleDBReplaceableAttributes(TodoItem item)
        {
            return new List<ReplaceableAttribute>() {
                new ReplaceableAttribute () {
                    Name = "Name",
                    Value = item.Name,
                    Replace = true
                },
                new ReplaceableAttribute () {
                    Name = "Link",
                    Value = item.Link,
                    Replace = true
                },
                new ReplaceableAttribute () {
                    Name = "Price",
                    Value = item.Price.ToString(),
                    Replace = true
                },
                new ReplaceableAttribute () {
                    Name = "Bought",
                    Value = item.Bought.ToString (),
                    Replace = true
                },
				/*new ReplaceableAttribute () {
					Name = "User",
					Value = App.User.Email,
					Replace = true
				}*/
			};
        }

        TodoItem FromSimpleDBAttributes(List<Amazon.SimpleDB.Model.Attribute> attributeList, string id)
        {
            var todoItem = new TodoItem();
            todoItem.ID = id;
            todoItem.Name = attributeList.Where(attr => attr.Name == "Name").FirstOrDefault().Value;
            todoItem.Link = attributeList.Where(attr => attr.Name == "Link").FirstOrDefault().Value;
            todoItem.Price = Convert.ToDouble(attributeList.Where(attr => attr.Name == "Price").FirstOrDefault().Value);
            todoItem.Bought = Convert.ToBoolean(attributeList.Where(attr => attr.Name == "Bought").FirstOrDefault().Value);
            return todoItem;
        }

        public async Task<List<TodoItem>> RefreshDataAsync()
        {
            var Items = new List<TodoItem>();

            try
            {
                var request = new SelectRequest()
                {
                    // The users email address is used to identify data in SimpleDB
                    SelectExpression = string.Format("SELECT * from {0}", tableName)
                };
                var response = await client.SelectAsync(request);
                foreach (var item in response.Items)
                {
                    Items.Add(FromSimpleDBAttributes(item.Attributes, item.Name));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return Items;
        }

        public async Task SaveTodoItemAsync(TodoItem todoItem)
        {
            try
            {
                var attributeList = ToSimpleDBReplaceableAttributes(todoItem);
                var request = new PutAttributesRequest()
                {
                    DomainName = tableName,
                    ItemName = todoItem.ID,
                    Attributes = attributeList
                };
                await client.PutAttributesAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTodoItemAsync(TodoItem todoItem)
        {
            try
            {
                var attributeList = ToSimpleDBAttributes(todoItem);
                var request = new DeleteAttributesRequest()
                {
                    DomainName = tableName,
                    ItemName = todoItem.ID,
                    Attributes = attributeList
                };
                await client.DeleteAttributesAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}
