using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoDataGrid
{
	public partial class MainWindow : Window
	{
		SqlConnection connection;
		SqlDataAdapter adapter;
		DataSet dataSet;

		public MainWindow()
		{
			InitializeComponent();

			using (var context = new GoodsContext())
			{

				context.Goods.Add(new Good
				{
					Id = 1,
					Name = "1001 способ приготовить банан",
					Price = 1001,
					Count = 2
				});

				context.SaveChanges();
			}


			try
			{
				connection = new SqlConnection();
				connection.ConnectionString = @"Data Source=sql;Initial Catalog=AutoDataGrid.GoodsContext;Integrated Security=True";
				connection.Open();

				adapter = new SqlDataAdapter("select Id, Name as 'Name', Price as 'Price', Count as 'Count' from Goods", connection);
				dataSet = new System.Data.DataSet();
				adapter.Fill(dataSet,"Good");
				goodsGrid.DataContext = dataSet.Tables[0];
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR:" + ex.Message);
			}
		}


		private void WindowLoad(object sender, RoutedEventArgs e)
		{
			using (var context = new GoodsContext())
			{

				context.Goods.Add(new Good
				{
					Id = 1,
					Name = "1001 способ приготовить банан",
					Price = 1001,
					Count = 2
				});

				context.Goods.Add(new Good
				{
					Id = 2,
					Name = "3003 способ приготовить банан",
					Price = 3003,
					Count = 3
				});


				context.SaveChanges();
			}


				try
			{
				connection = new SqlConnection();
				connection.ConnectionString = @"Data Source=sql;Initial Catalog=GoodsTest;Integrated Security=True";
				connection.Open();

				adapter = new SqlDataAdapter("select Id, Name as 'Name', Price as 'Price', Count as 'Count' from Goods", connection);
				dataSet = new System.Data.DataSet();
				adapter.Fill(dataSet, "Good");
				goodsGrid.DataContext = dataSet.Tables[0];
			}
			catch(Exception ex)
			{
				MessageBox.Show("ERROR:" + ex.Message);
			}

		}

		private void UpdateButton(object sender, RoutedEventArgs e)
		{
			try
			{
				SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
				adapter.Update(dataSet);
				MessageBox.Show("Info Updated");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
			}
		}
	}
}
