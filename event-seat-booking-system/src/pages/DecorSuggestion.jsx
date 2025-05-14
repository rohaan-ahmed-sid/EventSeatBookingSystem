import React, { useState } from "react";
import { getDecorSuggestionImage } from "../services/api";

const DecorSuggestion = () => {
    const [eventName, setEventName] = useState("");
    const [theme, setTheme] = useState("");
    const [imageUrl, setImageUrl] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setImageUrl("");
        try {
            const url = await getDecorSuggestionImage(eventName, theme);
            setImageUrl(url);
        } catch {
            alert("Failed to generate decor suggestion.");
        }
        setLoading(false);
    };

    return (
        <div style={{ maxWidth: 500, margin: "auto" }}>
            <h2>AI Decor Suggestion</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Event Name:</label>
                    <input
                        type="text"
                        value={eventName}
                        onChange={(e) => setEventName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Theme:</label>
                    <input
                        type="text"
                        value={theme}
                        onChange={(e) => setTheme(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? "Generating..." : "Get Decor Suggestion"}
                </button>
            </form>
            {imageUrl && (
                <div style={{ marginTop: 20 }}>
                    <h3>Suggested Decor Image:</h3>
                    <img src={imageUrl} alt="Decor Suggestion" style={{ maxWidth: "100%" }} />
                </div>
            )}
        </div>
    );
};

export default DecorSuggestion;