using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_eFILECompany
{
    public class ImageDetail
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public byte[] Img { get; set; }
    }
}
