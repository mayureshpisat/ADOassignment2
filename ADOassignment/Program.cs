using SubjectLibrary;



Type type = typeof(Subject);

var attributes = type.GetCustomAttributes(false);
Console.WriteLine(attributes);