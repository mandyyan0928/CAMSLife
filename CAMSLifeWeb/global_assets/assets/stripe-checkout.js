      // This is your test publishable API key.
const stripe = Stripe("pk_live_51Kb4GWF9jGaYbIVJgtXcQt7Cvjx6wilBUeOSS8Iebc9Xx6S0pTQcCL0vFRTaEP7Y8r3LPbPEybyWEIkXtJUpBFti00PeLEJTzS");

// The items the customer wants to buy
 
let elements;

initialize();
checkStatus();

document
  .querySelector("#payment-form")
  .addEventListener("submit", handleSubmit);

// Fetches a payment intent and captures the client secret
async function initialize() {
    setLoading(true);
    const response = await fetch("/Payment/CreateStripePayment", {
    method: "POST",
      headers: { "Content-Type": "application/json" },

    body: JSON.stringify({ items }),
    });

    var check = await response.json();
    
    const { clientSecret } = check;
   
  const appearance = {
    theme: 'stripe',
  };
  elements =await stripe.elements({ appearance, clientSecret });
     
  const paymentElement = elements.create("payment");
    paymentElement.mount("#payment-element");
    setLoading(false);
}

async function handleSubmit(e) {
    
  e.preventDefault();
    setLoading(true);
    
  const { error } = await  stripe.confirmPayment({
    elements,
    confirmParams: {
      // Make sure to change this to your payment completion page
      return_url: items.callback_url,
    },
  });

  // This point will only be reached if there is an immediate error when
  // confirming the payment. Otherwise, your customer will be redirected to
  // your `return_url`. For some payment methods like iDEAL, your customer will
  // be redirected to an intermediate site first to authorize the payment, then
  // redirected to the `return_url`.
  if (error.type === "card_error" || error.type === "validation_error") {
    showMessage(error.message);
  } else {
    showMessage("An unexpected error occurred.");
  }

  setLoading(false);
}

// Fetches the payment intent status after payment submission
async function checkStatus() {
    const clientSecret = new URLSearchParams(window.location.search).get(
        "payment_intent_client_secret"
    );

    if (!clientSecret) {
        return;
    }

    const { paymentIntent } = await stripe.retrievePaymentIntent(clientSecret);
    console.log(paymentIntent);
    switch (paymentIntent.status) {
        case "succeeded":
            showMessage("Payment succeeded!");
            break;
        case "processing":
            showMessage("Your payment is processing.");
            break;
        case "requires_payment_method":
            showMessage("Your payment was not successful, please try again.");
            break;
        default:
            showMessage("Something went wrong.");
            break;
    }
    var options = {
        url: '/Payment/Result/' + result.id + '/' + result.paymentChannel,
        method: 'POST',
        data: [{
            status: paymentIntent.status
        }]
    };
    url_redirect(options);
    alert(paymentIntent.status);
}


function url_redirect(options) {
    var $form = $("<form />");

    $form.attr("action", options.url);
    $form.attr("method", options.method);
    debugger;
    for (var data in options.data)
        $form.append('<input type="hidden" name="' + data + '" value="' + options.data[data] + '" />');

    $("body").append($form);
    $form.submit();
}

// ------- UI helpers -------

function showMessage(messageText) {
  const messageContainer = document.querySelector("#payment-message");

  messageContainer.classList.remove("hidden");
  messageContainer.textContent = messageText;

  setTimeout(function () {
    messageContainer.classList.add("hidden");
    messageText.textContent = "";
  }, 4000);
}

// Show a spinner on payment submission
function setLoading(isLoading) {
  if (isLoading) {
    // Disable the button and show a spinner
    document.querySelector("#submit").disabled = true;
    document.querySelector("#spinner").classList.remove("hidden");
    document.querySelector("#button-text").classList.add("hidden");
  } else {
    document.querySelector("#submit").disabled = false;
    document.querySelector("#spinner").classList.add("hidden");
    document.querySelector("#button-text").classList.remove("hidden");
  }
}