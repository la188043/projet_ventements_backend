using System;

namespace Application.Services.Users.Dto
{
    public class OutputDtoQueryUser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public bool Administrator { get; set; }
        public Address UserAddress { get; set; }

        public class Address
        {
            public int Id { get; set; }
            public string Street { get; set; }
            public int HomeNumber { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }

            private bool Equals(Address other)
            {
                return Id == other.Id && Street == other.Street &&
                       HomeNumber == other.HomeNumber && Zip == other.Zip &&
                       City == other.City;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Address) obj);
            }
        }

        private bool Equals(OutputDtoQueryUser other)
        {
            return Id == other.Id && Firstname == other.Firstname && Lastname == other.Lastname &&
                   Email == other.Email && Gender == other.Gender &&
                   Administrator == other.Administrator;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoQueryUser) obj);
        }
    }
}