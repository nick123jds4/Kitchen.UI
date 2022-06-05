using Kitchen.Model;
using System;
using System.Collections.Generic;

namespace Kitchen.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Customer>
    {
        public FriendWrapper(Customer model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FullName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Address
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string DateOfMeasurement
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FullName):
                    if (string.Equals(FullName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robots are not valid clients";
                    }
                    break;
            }
        }
    }
}
