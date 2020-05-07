using System;

namespace MyForum.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CascadeDeleteAttribute : Attribute
    {
    }
}
