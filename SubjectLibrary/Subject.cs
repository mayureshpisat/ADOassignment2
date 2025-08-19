namespace SubjectLibrary
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class IsTableAttribute : Attribute
    {
        public IsTableAttribute()
        {

        }
    }


    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple =false)]
    public class IsColumnAttribute : Attribute
    {
        string dataType;
        public IsColumnAttribute(string DataType)
        {
            dataType = DataType;
        }
    }


    [IsTable]
    public class Subject
    {
        [IsColumn("INT")]
        public int Id { get; set; }

        [IsColumn("VARCHAR(100)")]
        public string Description { get; set; }

        [IsColumn("VARCHAR(10)")]
        public string Code { get; set; }


    }
}
