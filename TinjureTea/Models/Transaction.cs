//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TinjureTea.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public Nullable<int> DistributorId { get; set; }
        public Nullable<int> RetailerId { get; set; }
        public string Date { get; set; }
        public Nullable<int> Balance { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> SalesId { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual Distributor Distributor { get; set; }
    }
}