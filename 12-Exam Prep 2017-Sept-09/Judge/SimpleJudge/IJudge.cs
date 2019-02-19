using System.Collections.Generic;

public interface IJudge
{
    void AddContest(int contestId);

    void AddSubmission(Submission submission);

    void AddUser(int userId);

    IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType);

    IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId);

    void DeleteSubmission(int submissionId);

    IEnumerable<int> GetContests();

    IEnumerable<Submission> GetSubmissions();

    IEnumerable<int> GetUsers();

    IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId);

    IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType);
}
