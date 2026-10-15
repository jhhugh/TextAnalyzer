class LetterData {
    constructor(letter, letterCount, letterPercentage) {
        this.Letter = letter;
        this.LetterCount = letterCount;
        this.LetterPercentage = letterPercentage;
    }
}

class AnalysisResponse {
    constructor(letters) {
        this.LetterData = letters;
    }

    static fromJson(data) {
        const raw = data.letterData ?? data.LetterData ?? [];
        const letters = raw.map(item =>
            new LetterData(
                item.letter ?? item.Letter,
                item.letterCount ?? item.LetterCount,
                item.letterPercentage ?? item.LetterPercentage
            )
        );
        return new AnalysisResponse(letters);
    }
}

class AnalysisRequest {
    constructor(textInput) {
        this.textInput = textInput;
    }
}