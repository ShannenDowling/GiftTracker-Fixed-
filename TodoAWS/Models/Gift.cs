using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAWSSimpleDB.Models
{
    class Gift
    {
        private String title;
        private double price;
        private String link;

        public Gift(string title, double price, string link)
        {
            this.title = title;
            this.price = price;
            this.link = link;
        }

        void setTitle(String title) { title = this.title; }
        void setPrice(double price) { price = this.price; }
        void setLink(String link) { link = this.link; }


        String getTitle() { return title; }
        double getPrice() { return price; }
        String getLink() { return link; }
    }
}
