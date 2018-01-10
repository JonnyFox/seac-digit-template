using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class FeedbackService
    {
        SeacDigitTemplateContext _ctx;

        public FeedbackService(SeacDigitTemplateContext ctx) => _ctx = ctx;

        public Task<List<Feedback>> GetAll() => _ctx.FeedbackList.ToListAsync();

        public Task<Feedback> GetByIdAsync(int id) => _ctx.FeedbackList.SingleOrDefaultAsync(i => i.Id == id);

        public async Task<int> DeleteByIdAsync(int id)
        {
            var feedback = await this.GetByIdAsync(id);
            if (feedback == null)
            {
                return 0;
            }
            _ctx.FeedbackList.Remove(feedback);
            return await _ctx.SaveChangesAsync();
        }

    }
}