using System.Net;
using System.Net.Sockets;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public class IPAddressRange
    {
        private readonly AddressFamily _addressFamily;
        private readonly byte[] _lowerBytes;
        private readonly byte[] _upperBytes;

        public IPAddressRange(IPAddress lowerInclusive, IPAddress upperInclusive)
        {
            // Assert that lower.AddressFamily == upper.AddressFamily

            this._addressFamily = lowerInclusive.AddressFamily;
            this._lowerBytes = lowerInclusive.GetAddressBytes();
            this._upperBytes = upperInclusive.GetAddressBytes();
        }

        public bool IsInRange(IPAddress address)
        {
            if (address.AddressFamily != _addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < this._lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < _lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > _upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == _lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == _upperBytes[i]);
            }

            return true;
        }
    }
}
