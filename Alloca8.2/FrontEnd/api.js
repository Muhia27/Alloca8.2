async function registerUser(userName, email, password, role) {
    const userData = {

      
        UserName: userName,
        Email: email,
        Password: password,
        Role: role
    };
    try {
        const response = await fetch("http://localhost:5056/api/Users/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(userData)
        });

        const result = await response.json();
        if (response.ok) {
            alert("User registered successfully!");
            window.location.href = "Sign In.html";

        } else {
            alert("Error:" + result.message);
        }
    }

        catch (error) {
            console.error("Error:", error);
            alert("Something went wrong .Check Console for details.");
        }
    }
