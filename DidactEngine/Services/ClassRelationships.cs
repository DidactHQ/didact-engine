
public class ClassRelationships
{
    public Class1[] classes { get; set; }
    public Relationship[] relationships { get; set; }
}

public class Class1
{
    public string name { get; set; }
    public int methods { get; set; }
    public int properties { get; set; }
}

public class Relationship
{
    public string from { get; set; }
    public string to { get; set; }
}
