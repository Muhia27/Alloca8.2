async function registerUser(userName, email, password, role, ownerID) {
    // Convert role string to number (matching the C# enum)
    const roleMapping = {
        "customer": 1,
        "hotelOwner": 2
    };

    const userData = {
        UserName: userName,
        Email: email,
        Password: password,
        Role: roleMapping[role] || 1, // Default to customer if invalid role
        OwnerID: ownerID === "null" ? null : ownerID // Include ownerID in the payload
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
            // Check if the user is a hotel owner and redirect accordingly
            if (role === "hotelOwner") {
                // Assuming your backend returns ownerID in the result (it should now)
                if (result.ownerID) {
                    localStorage.setItem('ownerID', result.ownerID);
                    window.location.href = "HotelRegistration.html"; // Redirect to hotel registration
                } else {
                    console.error("Owner ID not found in server response.");
                    alert("Registration successful, but owner ID was not received. Please sign in.");
                    window.location.href = "Sign In.html";
                }
            } else {
                window.location.href = "Sign In.html"; // Redirect to sign in for customers
            }
        } else {
            alert("❌ Error: " + (result.message || "Unknown error"));
        }
    } catch (error) {
        console.error("🚨 Network/Fetch Error:", error);
        alert("Something went wrong. Check the console for details.");
    }
}