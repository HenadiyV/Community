using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Abstract
{
   public interface IAddressRepository
    {
        IEnumerable<Address> Address {  get; }
        void SaveAddress(Address address);
        void AddressDelete(int Id);
        void AddAddress(Address address);
        Address GetAddress(int Id);
       void DefaultUserAddress(int Id);
        List<string> GetCity();
        List<User> GetSearchUserCity(string city);
    }
}
