namespace MaximTech.WebApi.Models.Responses;

public record StringProcessResult(
    string ReversedString,
    string Repetitions,
    string SubstringInBand,
    string Sorted,
    string Trimmed
);