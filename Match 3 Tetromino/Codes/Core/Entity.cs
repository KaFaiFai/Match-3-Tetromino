using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Core
{
    internal abstract class Entity
    {
        public List<Property> Properties { get; private set; } = new List<Property>();

        protected void AddProperty(Property property)
        {
            if (HasProperty(property.GetType()))
            {
                throw new ArgumentException("Property already exists");
            }
            Properties.Add(property);
        }

        public bool HasProperty(Type type)
        {
            return Properties.Any(c => c.GetType() == type);
        }

        public bool HasProperties(List<Type> types)
        {
            return types.All(c => HasProperty(c));
        }

        public Property GetProperty<T>() where T : Property
        {
            foreach (Property property in Properties)
            {
                if (property is T)
                {
                    return property;
                }
            }
            throw new ArgumentException("Property does not exist");
        }
    }
}
