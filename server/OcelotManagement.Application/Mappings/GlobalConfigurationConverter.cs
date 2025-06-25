using OcelotManagement.Application.Dtos.Ocelot;
using OcelotManagement.Domain.Entities;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace OcelotManagement.Application.Mappings
{
    internal class GlobalConfigurationConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            if (destinationType == typeof(OcelotManagement.Application.Dtos.Ocelot.GlobalConfigurationDto))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(GlobalConfigurationDto) && value is GlobalConfiguration concreteValue)
            {
                GlobalConfigurationDto result = null;
                if (concreteValue.Enabled)
                {
                    result = new GlobalConfigurationDto();
                    result.RequestIdKey = concreteValue.RequestIdKey;
                    if (concreteValue.ServiceDiscoveryEnabled == true)
                    {
                        result.ServiceDiscoveryProvider = new ServiceDiscoveryProviderDto()
                        {
                            Host = concreteValue.ServicediscoveryHost,
                            PollingInterval = concreteValue.ServicediscoveryPollinginterval,
                            Port = concreteValue.ServicediscoveryPort,
                            Type = concreteValue.ServicediscoveryType,
                        };
                    }
                    if (concreteValue.RatelimitDisableratelimitheaders == true)
                    {
                        result.RateLimitOptions = new GlobalRateLimitOptionDto()
                        {
                            ClientIdHeader = concreteValue.RatelimitClientidheader,
                            DisableRateLimitHeaders = concreteValue.RatelimitDisableratelimitheaders,
                            HttpStatusCode = concreteValue.RatelimitHttpstatuscode,
                            QuotaExceededMessage = concreteValue.RatelimitQuotaexceededmessage
                        };
                    }
                }
                return result;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}