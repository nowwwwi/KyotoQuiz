using KyotoQuiz.ViewModels;

namespace KyotoQuiz.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidAnswerCount(QuestionViewModel model)
        {
            var count = (model.IsOrderOneAnswer ? 1 : 0)
               + (model.IsOrderTwoAnswer ? 1 : 0)
               + (model.IsOrderThreeAnswer ? 1 : 0)
               + (model.IsOrderFourAnswer ? 1 : 0);

            return count == 1;
        }
    }
}
