#region namespace
using System;
#endregion
namespace EMAAR.Emaarcommunities.Foundation.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        #region constructor
        public ServiceAttribute()
        {

        }
        #endregion
        public ServiceAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }
        #region property
        public Lifetime Lifetime { get; set; } = Lifetime.Singleton;
        public Type ServiceType { get; set; }
        #endregion
    }
}