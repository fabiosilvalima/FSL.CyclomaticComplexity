using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public abstract class Tag
    {
        private readonly string _tagName;
        private readonly TagBuilder _tagBuilder;
        private List<string> _childTags { get; set; }

        public Tag(string tagName, params string[] childTags)
        {
            _tagBuilder = new TagBuilder(tagName);
            _tagName = tagName;
            _childTags = childTags?.ToList() ?? new List<string>();
        }

        public string TagName
        {
            get
            {
                return _tagBuilder.TagName;
            }
        }

        public virtual Tag AddCssClass(string cssClass)
        {
            _tagBuilder.AddCssClass(cssClass);

            return this;
        }

        public Tag MergeAttribute(string key, string value)
        {
            _tagBuilder.MergeAttribute(key, value);

            return this;
        }

        public virtual Tag BuildTag(params Tag[] tags)
        {
            return BuildTag("", tags);
        }

        public Tag BuildTag(string innerHtml, params Tag[] tags)
        {
            SetHtml(innerHtml);

            if (tags?.Length == 0)
            {
                return this;
            }

            var sb = new StringBuilder();
            foreach (var tag in tags)
            {
                sb.Append(RenderTag(tag));
            }

            return this;
        }

        private string RenderTag(Tag tag)
        {
            if (_childTags.Contains(tag.TagName))
            {
                return tag.RenderTag();
            }

            return "";
        }

        private void SetHtml(string innerHtml)
        {
            _tagBuilder.InnerHtml = _tagBuilder.InnerHtml ?? innerHtml;
        }

        public string RenderTag()
        {
            return _tagBuilder.ToString(TagRenderMode.EndTag);
        }
    }
}