using OneDriver.Framework.Module.Parameter;
using OneDriver.PowerSupply.Abstract.Channels;
using OneDriver.PowerSupply.Abstract.Contracts;
using OneDriver.PowerSupply.Abstract;
using System.Collections.ObjectModel;
using OneDriver.Framework.Libs.Validator;
using OneDriver.PowerSupply.Basic.Products;

namespace OneDriver.PowerSupply.Factory
{
    public enum PowerSupplyType
    {
        Virtual,
        Kd3005p
    }
    public class AbstractPowerSupply
    {
        public CommonDeviceParams Parameters { get;  }

        public AbstractPowerSupply(CommonDeviceParams parameters, IPowerSupply methods, ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>> elements)
        {
            Parameters = parameters;
            Methods = methods;
            Elements = elements;
        }

        public IPowerSupply Methods { get;  }
        public ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>> Elements { get; }
    }

    public class PowerSupplyFactory
    {
        public static AbstractPowerSupply? Create(PowerSupplyType deviceType)
        {
            AbstractPowerSupply? device = null;

            switch (deviceType)
            {
                case PowerSupplyType.Virtual:
                    break;
                case PowerSupplyType.Kd3005p:
                    var obj = new Basic.Device("PowerSupplyVirtual", new ComportValidator(), new Kd3005p());
                    device = new AbstractPowerSupply(obj.Parameters, obj, new ObservableCollection<BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>>());
                    foreach (var ch in obj.Elements)
                    {
                        var item = new BaseChannelWithProcessData<CommonChannelParams, CommonProcessData>(new CommonChannelParams(ch.Parameters.Name), new CommonProcessData())
                        {
                            Parameters = ch.Parameters,
                            ProcessData = ch.ProcessData
                        };
                        device.Elements.Add(item);
                    }

                    break;
            }
            return device;
        }
    }
}
