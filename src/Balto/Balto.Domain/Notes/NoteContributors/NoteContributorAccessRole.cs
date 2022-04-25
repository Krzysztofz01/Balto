using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Notes.NoteContributors
{
    public class NoteContributorAccessRole : ValueObject<NoteContributorAccessRole>
    {
        public ContributorAccessRole Value { get; private set; }

        private NoteContributorAccessRole() { }
        public NoteContributorAccessRole(ContributorAccessRole value)
        {
            Value = value;
        }

        public static implicit operator ContributorAccessRole(NoteContributorAccessRole contributorAccessRole) => contributorAccessRole.Value;
        public static implicit operator string(NoteContributorAccessRole contributorAccessRole) => Enum.GetName(typeof(ContributorAccessRole), contributorAccessRole.Value);

        public static NoteContributorAccessRole Default => new NoteContributorAccessRole(ContributorAccessRole.ReadOnly);
        public static NoteContributorAccessRole FromContributorAccessRole(ContributorAccessRole accessRole) => new NoteContributorAccessRole(accessRole);
    }

    public enum ContributorAccessRole
    {
        ReadOnly,
        ReadWrite
    }
}
