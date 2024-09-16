using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.Individual1.Register
{
    static class TaskUtils
    {
        // Calculate the total money contributed by the group
        public static decimal CalculateTotalContribution(List<Info> members)
        {
            /// Sum of all contributions
            return members.Sum(member => member.Contribution); 
        }

        // Find the member(s) who contributed the most
        public static List<Info> FindTopContributors(List<Info> members)
        {
            /// Find the maximum contribution
            decimal maxContribution = members.Max(member => member.Contribution);

            /// Return a list of all members who contributed the maximum amount
            return members.Where(member => member.Contribution == maxContribution).ToList();
        }
    }
}

