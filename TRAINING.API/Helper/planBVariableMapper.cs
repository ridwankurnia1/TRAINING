// VariableMapper.cs
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

public static class VariableMapper
{
    public static VariableDto ToDto(this ZVAR variable)
    {
        return new VariableDto
        {
            VariableId = variable.ZRRCID,
            User = variable.ZRCONO,
            Name = variable.ZRBRNO,
            Code = variable.ZRVANO,
            Value = variable.ZRVANA,
        };
    }

    public static ZVAR ToModel(this VariableDto dto)
    {
        return new ZVAR
        {
            ZRRCID = dto.VariableId,
            ZRCRUS = dto.User,
            ZRVANA = dto.Name,
            ZRVATY = dto.Code,
            ZRCONO = dto.Value,
        };
    }
}