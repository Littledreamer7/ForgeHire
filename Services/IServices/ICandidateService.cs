using ForgeHire.Dtos.Candidate;

public interface ICandidateService
{
    Task RegisterCandidateAsync(CandidateSignUpDto dto);
}