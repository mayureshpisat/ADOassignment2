using SubjectLibrary;
using System.Reflection;
using System.Data.SqlClient;





Type type = typeof(Subject);

var isTableAttr = type.GetCustomAttributes(typeof(IsTableAttribute), false); //check is Subject is a table

string queryString = "Create Table SubjectsTable ( ";

if (isTableAttr.Length > 0)
{
    //get the properties of Subject class
    var properties = type.GetProperties();

    //Iterate through properties to get info about properties
    for(int i = 0;i < properties.Length; i++)
    {
        var propertyAttribute = (IsColumnAttribute)properties[i].GetCustomAttribute(typeof(IsColumnAttribute), false);

        var columnName = properties[i].Name;
        queryString += $"{columnName} {propertyAttribute.dataType} ";
        if (i < properties.Length - 1)
        {
            queryString += ", ";
        }
    }
    //foreach(var property in properties)
    //{
        

    //}
    queryString += ");";
}
else
{
    throw new Exception("Subject is not a table");
}

Console.WriteLine(queryString);


//Db connection sting 
string dbConnectionString = "Data Source=LAPTOP-LLA7QMGI\\SQLEXPRESS;Initial Catalog=Subjects;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";


//connect to sql server
using (SqlConnection connect = new SqlConnection(dbConnectionString))
{

    try
    {
        connect.Open();
        using (SqlCommand cmd = new SqlCommand(queryString, connect))
        {
            cmd.ExecuteNonQuery();


        }
        
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}
