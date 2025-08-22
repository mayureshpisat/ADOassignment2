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
        var length = propertyAttribute.length;
        if (length != null)
            queryString += $"{columnName} {propertyAttribute.dataType}({propertyAttribute.length}) ";
        else
            queryString += $"{columnName} {propertyAttribute.dataType} ";
         
        //primary key logic
        if(propertyAttribute.isPrimary == true)
        {
            if(propertyAttribute.dataType == "INT") // add seed and inc values only for primary keys with INT datatype
            {
                int seed = propertyAttribute.seedValue>0 ? propertyAttribute.seedValue : 1;
                int inc = propertyAttribute.increment>0 ? propertyAttribute.increment : 1;

                queryString += $"IDENTITY({seed},{inc}) PRIMARY KEY ";
            }
            else
            {
                queryString += $"PRIMARY KEY ";

            }
        }


        if (i < properties.Length - 1)
        {
            queryString += ", ";
        }


        
    }
    queryString += ");";
}
else
{
    throw new Exception("Subject is not a table");
}

Console.WriteLine(queryString);


////Db connection sting 
string dbConnectionString = "Data Source=DESKTOP-53KKGG8\\MSSQLSERVER02;Initial Catalog=AdoDotNetDb;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";


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
