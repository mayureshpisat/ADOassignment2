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
        public string dataType { get; }
        public string length { get; set; } //default null
        public bool isPrimary  { get; set; } //default null

        public int seedValue { get; set; } //default = 0

        public int increment { get; set; }

        public IsColumnAttribute(string dataType)
        {
            this.dataType = dataType;

            if(dataType == "INT")
            {
                length = null;
            }

            if (isPrimary == true && seedValue==0 && increment == 0)
            {
                seedValue = 1;
                increment = 1;
            }
        }
    }


    [IsTable]
    public class Subject
    {
        [IsColumn("INT", isPrimary =true, seedValue =1)]
        public int Id { get; set; }

        [IsColumn("VARCHAR", length ="100")]
        public string Description { get; set; }

        [IsColumn("VARCHAR", length ="50")]
        public string Code { get; set; }


    }
}
