﻿using yogaAshram.Controllers;

namespace yogaAshram.Models.ModelViews
{
    public class ClientsCreateModelView
    {
        public string NameSurname { get; set; }

        public string PhoneNumber { get; set; }

        public ClientType ClientType { get; set; } = ClientType.Probe;

        public long GroupId { get; set; }
        
        public virtual Group Group { get; set; }
    }
}