using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace Host.Model
{
    public class DependencyFactory
    {
        public static IUnityContainer Container { get; private set; }
        
        static DependencyFactory()
        {
            var container = new UnityContainer();
            
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section?.Configure(container);
            Container = container;
        }
        
        public static T Resolve<T>()
        {
            var ret = default(T);
 
            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }
 
            return ret;
        }
    }
}
