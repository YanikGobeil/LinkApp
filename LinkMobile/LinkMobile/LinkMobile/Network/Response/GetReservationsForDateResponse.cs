using LinkMobile.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkMobile.Network.Response
{
    public class GetReservationsForDateResponse : IEnumerable<Reservation>
    {
        private List<Reservation> _list = new List<Reservation>();

        public IEnumerator<Reservation> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
