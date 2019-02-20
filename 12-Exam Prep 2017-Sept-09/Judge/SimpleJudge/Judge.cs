using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    private readonly HashSet<int> users = new HashSet<int>();
    private readonly HashSet<int> contests = new HashSet<int>();
    private readonly Dictionary<int, Submission> submissionsById = new Dictionary<int, Submission>();

    public void AddContest(int contestId)
    {
        if (this.contests.Contains(contestId))
        {
            return;
        }

        this.contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        if (submission == null
            || !this.users.Contains(submission.UserId)
            || !this.contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }

        if (this.submissionsById.ContainsKey(submission.Id))
        {
            return;
        }

        this.submissionsById[submission.Id] = submission;
    }

    public void AddUser(int userId)
    {
        if (this.users.Contains(userId))
        {
            return;
        }

        this.users.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!this.submissionsById.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }

        this.submissionsById.Remove(submissionId);
    }

    public IEnumerable<int> GetContests()
        => this.contests.OrderBy(c => c);

    public IEnumerable<Submission> GetSubmissions()
        => this.submissionsById.Values
        .OrderBy(s => s.Id);

    public IEnumerable<int> GetUsers()
        => this.users.OrderBy(u => u);

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
        => this.submissionsById.Values
        .Where(s => s.UserId == userId)
        .OrderByDescending(s => s.Points)
        .ThenBy(s => s.Id)
        .Select(s => s.ContestId)
        .Distinct();

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
        => this.submissionsById.Values
        .Where(s => s.Type == submissionType)
        .Select(s => s.ContestId)
        .Distinct();

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        var results = this.submissionsById.Values
            .Where(s => s.UserId == userId)
            .Where(s => s.ContestId == contestId)
            .Where(s => s.Points == points);

        if (!results.Any())
        {
            throw new InvalidOperationException();
        }

        return results;
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
        => this.submissionsById.Values
        .Where(s => s.Type == submissionType)
        .Where(s => minPoints <= s.Points && s.Points <= maxPoints);
}
