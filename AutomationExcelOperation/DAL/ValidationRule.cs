//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ValidationRule
    {
        public int Id { get; set; }
        public string ExpenseTypeCd { get; set; }
        public string TriggeredOn { get; set; }
        public bool IsEnabledInd { get; set; }
        public string SeverityDesc { get; set; }
        public string RuleNm { get; set; }
        public string ExpenseSubTypeValue { get; set; }
        public Nullable<int> ValidationRuleActionTypeGroupNbr { get; set; }
        public string ErrorMsgDesc { get; set; }
        public Nullable<int> Sequence { get; set; }
    }
}
