async function registerUser(userName, email, password, role) {
    // Convert role string to number (matching the C# enum)
    const roleMapping = {
        "customer": 1,
        "hotelOwner": 2
    };

    const userData = {
        UserName: userName,
        Email: email,
        Password: password,
        Role: roleMapping[role] || 1 // Default to customer if invalid role
    };

    console.log("📤 Sending Data:", JSON.stringify(userData)); // Debugging
    try {
        const response = await fetch("http://localhost:5056/api/Users/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(userData)
        });

        console.log("🔄 Response Status:", response.status);
        const result = await response.json();
        console.log("📥 Server Response:", JSON.stringify(result));

        if (response.ok) {
            alert("✅ User registered successfully!");
            window.location.href = "Sign In.html";
        } else {
            alert("❌ Error: " + (result.message || "Unknown error"));
        }
    } catch (error) {
        console.error("🚨 Network/Fetch Error:", error);
        alert("Something went wrong. Check the console for details.");
    }
}