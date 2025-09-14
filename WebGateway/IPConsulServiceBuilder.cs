﻿using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;
using System.Xml.Linq;

namespace WebGateway
{
    public class IPConsulServiceBuilder : DefaultConsulServiceBuilder
    {

        public IPConsulServiceBuilder(Func<ConsulRegistryConfiguration> configurationFactory, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory) : base(configurationFactory, clientFactory, loggerFactory)
        {
        }

        protected override string GetDownstreamHost(ServiceEntry entry, Node node)
            => entry.Service.Address;
    }

}
