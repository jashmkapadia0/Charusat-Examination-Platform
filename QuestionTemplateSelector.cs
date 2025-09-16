using System.Windows;
using System.Windows.Controls;

namespace CharuEval.Models
{
    public class QuestionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MCQTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate CodeTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var question = item as ExamQuestion;
            if (question == null)
                return base.SelectTemplate(item, container);

            switch (question.Type)
            {
                case QuestionType.MCQ:
                    return MCQTemplate;
                case QuestionType.Text:
                    return TextTemplate;
                case QuestionType.Coding:
                    return CodeTemplate;
                default:
                    return base.SelectTemplate(item, container);
            }
        }
    }
}
