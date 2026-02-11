document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");

    loginForm.addEventListener("submit", async function (e) {
        e.preventDefault(); // prevent default form submission

        const data = {
            email: document.getElementById("Email").value,
            password: document.getElementById("Password").value,
        };

        try {
            const response = await fetch("/SignUp/Login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

            const result = await response.json();

            if (result.success) {
                alert(result.message);
                // redirect based on role
                if (result.role === "Admin") {
                    window.location.href = "/Admin/Dashboard";
                } else if (result.role === "Staff") {
                    window.location.href = "/Staff/Dashboard";
                } else {
                    window.location.href = "/Customer/Index";
                }
            } else {
                alert(result.message);
            }
        } catch (err) {
            console.error(err);
            alert("Something went wrong!");
        }
    });
});