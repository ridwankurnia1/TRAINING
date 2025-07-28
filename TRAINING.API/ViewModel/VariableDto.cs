// VariableDto.cs 
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace TRAINING.API.ViewModel
{
    public class VariableDto
    {
        public int VariableId { get; set; } 

        public string User { get; set; } = "AMG";

        public string Name { get; set; } = "CKP";

        public string Code { get; set; }

        public string Value { get; set; }
    }
}