namespace Sample
{

public readonly struct Marvel
{
    // No setter method included.
    public string CharacterName { get; }//*Correct*
    // `this` variable can be changed in 
    // constructor
    public Marvel(string name)
    {
        this.CharacterName = name;//*CORRECT*
    }
}




public record class Person {
   public string FirstName { get; set; }
   public string LastName { get; set; }
}

public record class Person {
   public string FirstName { get; init; }
   public string LastName { get; init; }
}



public record Person { 
   public string FirstName { get; init; } 
   public string LastName { get; init; } 
}
public sealed record Student : Person { 
   public int ID { get; init; } 
}
public sealed record Student : Person { 
   public int ID { get; init; } 
}






public record ToDo(string Description, bool IsDone, string Category = "Default");
    
public class Temp
{
    ToDo myTask = new ToDo("Decompile me", false);
}

record Person {
   public Person(string firstName, string lastName) {
      this.FirstName = firstName;
      this.LastName = lastName;
   }
   public string FirstName { get; }
   public string LastName { get; }
}
















public record ToDo(string Description, bool IsDone);




public class ToDo : IEquatable<ToDo>
  {
    private readonly string \u003CDescription\u003Ek__BackingField;
    private readonly bool \u003CIsDone\u003Ek__BackingField;

    public ToDo(string Description, bool IsDone)
    {
      this.\u003CDescription\u003Ek__BackingField = Description;
      this.\u003CIsDone\u003Ek__BackingField = IsDone;
      base.\u002Ector();
    }

    protected virtual Type EqualityContract
    {
      get
      {
        return typeof (ToDo);
      }
    }

    public string Description
    {
      get
      {
        return this.\u003CDescription\u003Ek__BackingField;
      }
      init
      {
        this.\u003CDescription\u003Ek__BackingField = value;
      }
    }

    public bool IsDone
    {
      get
      {
        return this.\u003CIsDone\u003Ek__BackingField;
      }
      init
      {
        this.\u003CIsDone\u003Ek__BackingField = value;
      }
    }

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();
      builder.Append(nameof (ToDo));
      builder.Append(" { ");
      if (this.PrintMembers(builder))
        builder.Append(' ');
      builder.Append('}');
      return builder.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Description = ");
      builder.Append((object) this.Description);
      builder.Append(", IsDone = ");
      builder.Append(this.IsDone.ToString());
      return true;
    }

    public static bool operator !=(ToDo left, ToDo right)
    {
      return !(left == right);
    }

    public static bool operator ==(ToDo left, ToDo right)
    {
      if ((object) left == (object) right)
        return true;
      return (object) left != null && left.Equals(right);
    }

    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.\u003CDescription\u003Ek__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.\u003CIsDone\u003Ek__BackingField);
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as ToDo);
    }

    public virtual bool Equals(ToDo other)
    {
      if ((object) this == (object) other)
        return true;
      return (object) other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.\u003CDescription\u003Ek__BackingField, other.\u003CDescription\u003Ek__BackingField) && EqualityComparer<bool>.Default.Equals(this.\u003CIsDone\u003Ek__BackingField, other.\u003CIsDone\u003Ek__BackingField);
    }

    public virtual ToDo \u003CClone\u003E\u0024()
    {
      return new ToDo(this);
    }

    protected ToDo(ToDo original)
    {
      base.\u002Ector();
      this.\u003CDescription\u003Ek__BackingField = original.\u003CDescription\u003Ek__BackingField;
      this.\u003CIsDone\u003Ek__BackingField = original.\u003CIsDone\u003Ek__BackingField;
    }

    public void Deconstruct(out string Description, out bool IsDone)
    {
      Description = this.Description;
      IsDone = this.IsDone;
    }
  }


public readonly record struct ToDoStruct(string Description, bool IsDone);


public record struct ToDo(string Description, bool IsDone);





public struct ToDo : IEquatable<ToDo>
  {
    private string \u003CDescription\u003Ek__BackingField;
    private bool \u003CIsDone\u003Ek__BackingField;

    public ToDo(string Description, bool IsDone)
    {
      this.\u003CDescription\u003Ek__BackingField = Description;
      this.\u003CIsDone\u003Ek__BackingField = IsDone;
    }

    public string Description
    {
      readonly get
      {
        return this.\u003CDescription\u003Ek__BackingField;
      }
      set
      {
        this.\u003CDescription\u003Ek__BackingField = value;
      }
    }

    public bool IsDone
    {
      readonly get
      {
        return this.\u003CIsDone\u003Ek__BackingField;
      }
      set
      {
        this.\u003CIsDone\u003Ek__BackingField = value;
      }
    }







public record ToDo(string Description)
{
    public ToDo(string description, bool isDone) : this(description)
            
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));
        IsDone = isDone;
    }
    
    public bool IsDone { get; init; }
}







public class ToDo : IEquatable<ToDo>
  {
    private readonly string \u003CDescription\u003Ek__BackingField;
    private readonly bool \u003CIsDone\u003Ek__BackingField;

    public ToDo(string Description)
    {
      this.\u003CDescription\u003Ek__BackingField = Description;
      base.\u002Ector();
    }

    protected virtual Type EqualityContract
    {
      get
      {
        return typeof (ToDo);
      }
    }

    public string Description
    {
      get
      {
        return this.\u003CDescription\u003Ek__BackingField;
      }
      init
      {
        this.\u003CDescription\u003Ek__BackingField = value;
      }
    }

    public ToDo(string description, bool isDone)
    {
      this.\u002Ector(description);
      if (string.IsNullOrWhiteSpace(description))
        throw new ArgumentException("Description is required", nameof (description));
      this.IsDone = isDone;
    }

    public bool IsDone
    {
      get
      {
        return this.\u003CIsDone\u003Ek__BackingField;
      }
      init
      {
        this.\u003CIsDone\u003Ek__BackingField = value;
      }
    }

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();
      builder.Append(nameof (ToDo));
      builder.Append(" { ");
      if (this.PrintMembers(builder))
        builder.Append(' ');
      builder.Append('}');
      return builder.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Description = ");
      builder.Append((object) this.Description);
      builder.Append(", IsDone = ");
      builder.Append(this.IsDone.ToString());
      return true;
    }

    public static bool operator !=(ToDo left, ToDo right)
    {
      return !(left == right);
    }

    public static bool operator ==(ToDo left, ToDo right)
    {
      if ((object) left == (object) right)
        return true;
      return (object) left != null && left.Equals(right);
    }

    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.\u003CDescription\u003Ek__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.\u003CIsDone\u003Ek__BackingField);
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as ToDo);
    }

    public virtual bool Equals(ToDo other)
    {
      if ((object) this == (object) other)
        return true;
      return (object) other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.\u003CDescription\u003Ek__BackingField, other.\u003CDescription\u003Ek__BackingField) && EqualityComparer<bool>.Default.Equals(this.\u003CIsDone\u003Ek__BackingField, other.\u003CIsDone\u003Ek__BackingField);
    }

    public virtual ToDo \u003CClone\u003E\u0024()
    {
      return new ToDo(this);
    }

    protected ToDo(ToDo original)
    {
      base.\u002Ector();
      this.\u003CDescription\u003Ek__BackingField = original.\u003CDescription\u003Ek__BackingField;
      this.\u003CIsDone\u003Ek__BackingField = original.\u003CIsDone\u003Ek__BackingField;
    }

    public void Deconstruct(out string Description)
    {
      Description = this.Description;
    }
  }
}














