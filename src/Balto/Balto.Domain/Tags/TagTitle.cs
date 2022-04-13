using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Tags
{
    public class TagTitle : ValueObject<TagTitle>
    {
        private const int _maxLength = 100;

        public string Value { get; private set; }

        private TagTitle() { }
        public TagTitle(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid project tag title length.");

            Value = value;
        }


        public static implicit operator string(TagTitle title) => title.Value;

        public static TagTitle FromString(string title) => new TagTitle(title);
    }
}
