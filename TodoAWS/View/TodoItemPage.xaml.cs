﻿using System;
using Xamarin.Forms;

namespace TodoAWSSimpleDB
{
	public partial class TodoItemPage : ContentPage
	{
		public TodoItemPage ()
		{
			InitializeComponent ();
		}

		async void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.SaveTaskAsync(todoItem);
			await Navigation.PopAsync();

            await Navigation.PushAsync(new TodoListPage());
        }

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.DeleteTaskAsync(todoItem);
			await Navigation.PopAsync();
		}

		async void OnCancelActivated (object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}