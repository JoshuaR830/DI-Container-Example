using System;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyInjection
{
    public class DiContainer
    {
        // Store the types (concrete implementation) by interface
        private Dictionary<Type, Type> _types = new Dictionary<Type, Type>();

        // register the interface with the type
        public void Register<TInterface, TType>()
        {
            // Check that it has not already been registered
            if (_types.ContainsKey(typeof(TInterface)))
            {
                throw new Exception("Type is already registered");
            }
            
            _types.Add(typeof(TInterface), typeof(TType));
        }

        public TInterface Resolve<TInterface>()
        {
            // Returns the concrete implementation required for the type
            return (TInterface) GetImplementation(typeof(TInterface));
        }

        
        // Returns the most generic type
        private object GetImplementation(Type type)
        {
            // Check if it exists
            if (!_types.ContainsKey(type))
            {
                throw new Exception("Type does not exist");
            }
            
            // Get actual value (concrete implementation) or default value of null - but we know that it exists and is not null
            Type implementation = _types.GetValueOrDefault(type);

            if(implementation == null) 
                throw new Exception("Type does not exist");

            // Reflection - get metadata about our code
            ConstructorInfo constructorInfo = implementation.GetConstructors()[0];
            
            
            // Has the parameters that can be accepted which are the dependencies
            var constructorParamTypes = constructorInfo.GetParameters();
            
            // Now we only know the types, but we need the concrete implementations
            // the concrete implementations are registered in the DI container
            
            // List of the concrete implementations of the dependencies required
            List<object> constructorParamImplementations = new List<object>();

            foreach (var param in constructorParamTypes)
            {
                // Get instance of them and use to instantiate type that we are resolving
                // Recursively call GetImplementation in order to build up all of the dependencies
                constructorParamImplementations.Add(GetImplementation(param.ParameterType));
            }

            // Passed to the constructor - actually calls the constructor
            return constructorInfo.Invoke(constructorParamImplementations.ToArray());

        }
    }
}