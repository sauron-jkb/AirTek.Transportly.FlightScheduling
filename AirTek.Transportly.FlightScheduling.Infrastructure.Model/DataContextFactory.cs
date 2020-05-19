using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace AirTek.Transportly.FlightScheduling.Infrastructure.Model
{
	public class DataContextFactory : Disposable, IDataContextFactory
	{
        private DataContext _dataContext;
        private IConfiguration _configuration;

        public DataContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataContext GetDataContext()
        {
            if (_dataContext == null)
            {
                Type dataContextType = Type.GetType(_configuration["AppSettings:DataContextType"]);

                object[] instanceArgs = new object[]
                {

                };

                _dataContext = (DataContext)Activator.CreateInstance(dataContextType, instanceArgs);
            }

            return _dataContext;
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
            }
        }
    }
}
