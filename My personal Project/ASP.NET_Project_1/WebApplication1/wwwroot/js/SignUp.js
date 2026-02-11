document.addEventListener("DOMContentLoaded", function () {

    const signupForm = document.getElementById("signUpForm");
    const messageBox = document.getElementById("messageBox");

    if (!signupForm) return;

    signupForm.addEventListener("submit", async function (e) {
        e.preventDefault();

        // Clear old messages
        messageBox.innerHTML = "";

        // Get form values
        const fullName = document.getElementById("FullName").value.trim();
        const email = document.getElementById("Email").value.trim();
        const phone = document.getElementById("Phone").value.trim();
        const password = document.getElementById("Password").value;
        const confirmPassword = document.getElementById("ConfirmPassword").value;

        // ==========================
        // Frontend Validation
        // ==========================

        if (!fullName || !email || !phone || !password || !confirmPassword) {
            showMessage("All fields are required.", "danger");
            return;
        }

        if (password !== confirmPassword) {
            showMessage("Passwords do not match.", "danger");
            return;
        }

        if (password.length < 6) {
            showMessage("Password must be at least 6 characters.", "danger");
            return;
        }

        // ==========================
        // Disable button while loading
        // ==========================

        const submitBtn = signupForm.querySelector("button[type='submit']");
        submitBtn.disabled = true;
        submitBtn.innerHTML = "Processing...";

        // ==========================
        // Send Data to Server
        // ==========================

        const data = {
            fullName: fullName,
            email: email,
            phone: phone,
            password: password,
            confirmPassword: confirmPassword
        };

        try {
            const response = await fetch("/SignUp/Register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

            const result = await response.json();

            if (result.success) {
                showMessage(result.message, "success");

                // Redirect after 1.5 seconds
                setTimeout(() => {
                    window.location.href = "/SignUp/Login";
                }, 1500);
            } else {
                showMessage(result.message, "danger");
            }

        } catch (error) {
            console.error(error);
            showMessage("Something went wrong. Please try again.", "danger");
        } finally {
            submitBtn.disabled = false;
            submitBtn.innerHTML = "Sign Up";
        }
    });

    // ==========================
    // Helper Function
    // ==========================

    function showMessage(message, type) {
        messageBox.innerHTML = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
    }
});