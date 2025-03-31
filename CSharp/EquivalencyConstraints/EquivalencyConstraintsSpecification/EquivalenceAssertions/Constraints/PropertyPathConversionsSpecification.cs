using System.Collections;
using EquivalencyConstraints.EquivalenceAssertions.Constraints;
using EquivalencyConstraintsSpecification.E2E.Fixture;

namespace EquivalencyConstraintsSpecification.EquivalenceAssertions.Constraints;

[TestFixture]
[TestOf(typeof(PropertyPathConversions))]
public class PropertyPathConversionsSpecification
{

  [Test]
  public void ShouldWHAT() //bug
  {
    Assert.Multiple(() =>
    {
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<int, Company>(c => c.Director.Age),
        Is.EqualTo("Director.Age"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<string, Company>(p => p.Director.Name),
        Is.EqualTo("Director.Name"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<string, Company>(p => p.name),
        Is.EqualTo("Director.name"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<int, Person2>(p => p.Children[0].ID),
        Is.EqualTo("Children[0].ID"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<int, Person2>(p => p.Children[1].ID),
        Is.EqualTo("Children[1].ID"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<Child, Person2>(p => p.Children[0]),
        Is.EqualTo("Children[0]"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<List<Child>, Person2>(p => p.Children),
        Is.EqualTo("Children"));
      Assert.That(PropertyPathConversions.GetPropertyPathFrom<string, Person>(p => p.Name),
        Is.EqualTo("Name"));
    });
  }
}

public class TreeNode
{
  public string Name { get; set; }
  public List<TreeNode> Children { get; set; }
}

  // Represents a node in the tree
  public class Node
  {
    public Type Type { get; }
    public string Name { get; }
    public string Path { get; }
    public List<Node> Children { get; }

    public Node(Type type, string name, string path)
    {
      Type = type;
      Name = name;
      Path = path;
      Children = new List<Node>();
    }
  }

  // Static class to convert objects to a tree of nodes
  public static class NodeConverter
  {
    /// <summary>
    /// Converts an object into a tree of nodes.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>The root node of the tree.</returns>
    public static Node ToNodes(object obj)
    {
      return CreateNode(obj, null, "");
    }

    private static Node CreateNode(object obj, string name, string path)
    {
      // Get the type of the object, null if the object is null
      Type type = obj?.GetType();
      var node = new Node(type, name, path);

      // Handle null or primitive types (no children)
      if (obj == null || IsPrimitive(type))
      {
        return node;
      }
      // Handle collections
      else if (IsCollection(type))
      {
        int index = 0;
        foreach (var item in (IEnumerable)obj)
        {
          string childName = $"[{index}]";
          string childPath = path + childName;
          var childNode = CreateNode(item, childName, childPath);
          node.Children.Add(childNode);
          index++;
        }
      }
      // Handle complex types (classes or structs)
      else
      {
        var properties = type.GetProperties().Where(p => p.CanRead);
        foreach (var prop in properties)
        {
          object propValue = prop.GetValue(obj);
          string childName = prop.Name;
          // If root path is empty, use property name; otherwise, append with colon
          string childPath = path == "" ? childName : path + ":" + childName;
          var childNode = CreateNode(propValue, childName, childPath);
          node.Children.Add(childNode);
        }
      }
      return node;
    }

    // Determines if a type is primitive (leaf node)
    private static bool IsPrimitive(Type type)
    {
      if (type == null) return true;
      return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(decimal);
    }

    // Determines if a type is a collection but not a string
    private static bool IsCollection(Type type)
    {
      return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
    }
  }

  public class Company2
  {
    public Person2 Director { get; set; }
    public List<int> Grades { get; set; }
  }

  // Example usage
  public class SomeTests
  {
    [Test]
    public void Lol()
    {
      var company = new Company2
      {
        Director = new Person2 { Name = "Zenek" },
        Grades = new List<int> { 1, 2 }
      };

      Node root = NodeConverter.ToNodes(company);
      PrintNode(root, 0);
    }

    // Helper method to print the tree (for demonstration)
    private static void PrintNode(Node node, int indent)
    {
      string indentStr = new string(' ', indent * 2);
      Console.WriteLine($"{indentStr}Node:");
      Console.WriteLine($"{indentStr}  Type: {node.Type?.Name ?? "null"}");
      Console.WriteLine($"{indentStr}  Name: {node.Name ?? "null"}");
      Console.WriteLine($"{indentStr}  Path: \"{node.Path}\"");
      Console.WriteLine($"{indentStr}  Children:");
      foreach (var child in node.Children)
      {
        PrintNode(child, indent + 1);
      }
    }
  }
