using System;
using System.Web.Http;
using recruit_dotnetframework.Models;
using recruit_dotnetframework.Services;

namespace recruit_dotnetframework.Controllers
{
    /// <summary>
    /// API for managing credit cards.
    /// </summary>
    [RoutePrefix("api/creditcard")]
    public class CreditCardController : ApiController
    {
        private readonly ICreditCardRepository _repository;

        public CreditCardController(ICreditCardRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Add a new credit card.
        /// </summary>
        /// <param name="card">The credit card to add.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreditCard card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _repository.Add(card);
                return Created(new Uri(Request.RequestUri, card.CardNumber), card);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get credit card by card number.
        /// </summary>
        /// <param name="cardNumber">The credit card number to retrieve.</param>
        /// <returns>The credit card details if found; otherwise, 404 Not Found.</returns>
        [HttpGet]
        [Route("{cardNumber}")]
        public IHttpActionResult Get(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return BadRequest("Card number cannot be empty.");

            try
            {
                var card = _repository.Get(cardNumber);
                if (card == null)
                    return NotFound();

                return Ok(card);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
