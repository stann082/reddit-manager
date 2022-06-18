using NUnit.Framework;
using System.Reflection;

namespace TestEnvironment
{
    public abstract class AbstractTest
    {

        #region Constructors

        protected AbstractTest()
        {
            // TODO: disable logging
            //Log.Logger = null;
        }

        #endregion

        #region Shared Methods

        protected void OverrideProperty<T>(T data, string propertyName, object value)
        {
            PropertyInfo propertyInfo = data.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null || !propertyInfo.CanWrite)
            {
                Assert.Fail($"Property \"{propertyName}\" not found...");
            }

            propertyInfo.SetValue(data, value, null);
        }

        #endregion

    }
}
