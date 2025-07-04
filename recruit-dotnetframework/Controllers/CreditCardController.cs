using System;
using System.Web.Http;
using recruit_dotnetframework.Services;
using recruit_dotnetframework.DTOs;

namespace recruit_dotnetframework.Controllers
{
    /// <summary>
    /// API controller for managing credit cards.
    /// </summary>
    [RoutePrefix("api/creditcard")]
    public class CreditCardController : ApiController
    {
        private readonly ICreditCardService _service;

        /// <summary>
        /// Constructor with dependency injection of service layer.
        /// </summary>
        /// <param name="service">Credit card service interface</param>
        public CreditCardController(ICreditCardService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new credit card record.
        /// </summary>
        /// <param name="request">Credit card details from client</param>
        /// <returns>Created credit card details with 201 status, or validation/server error</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] CreditCardRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdCard = _service.CreateCreditCard(request);
                return Created(new Uri(Request.RequestUri, createdCard.CardNumber), createdCard);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Retrieve credit card details by card number.
        /// </summary>
        /// <param name="cardNumber">The credit card number to search for</param>
        /// <returns>Credit card details if found; 404 Not Found otherwise</returns>
        [HttpGet]
        [Route("{cardNumber}")]
        public IHttpActionResult GetCreditCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return BadRequest("Card number cannot be empty.");

            try
            {
                var card = _service.GetCreditCard(cardNumber);
                if (card == null)
                    return NotFound();

                return Ok(card);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update an existing credit card.
        /// </summary>
        /// <param name="cardNumber">Card number to update.</param>
        /// <param name="request">Updated credit card details.</param>
        /// <returns>Updated credit card details or error.</returns>
        [HttpPut]
        [Route("{cardNumber}")]
        public IHttpActionResult Update(string cardNumber, [FromBody] CreditCardRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedCard = _service.UpdateCreditCard(cardNumber, request);
                return Ok(updatedCard);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete a credit card by card number.
        /// </summary>
        /// <param name="cardNumber">Card number to delete.</param>
        /// <returns>Success or error response.</returns>
        [HttpDelete]
        [Route("{cardNumber}")]
        public IHttpActionResult Delete(string cardNumber)
        {
            try
            {
                _service.DeleteCreditCard(cardNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
