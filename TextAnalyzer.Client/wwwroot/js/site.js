const textInput = document.getElementById('text-input');
const submitBtn = document.getElementById('submit-btn');
const statusElement = document.getElementById('status');
const resultsElement = document.getElementById('results');

textInput.addEventListener('input', () => {
	submitBtn.disabled = textInput.value.trim().length === 0;
});

submitBtn.addEventListener('click', async () => {
	const request = new AnalysisRequest(textInput.value);
	resultsElement.innerHTML = '';

	try {
		const res = await fetch('/api/analysis', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify(request)
		});

		if (!res.ok) {
			throw new Error(res.status);
		}

		const data = await res.json();
		const response = AnalysisResponse.fromJson(data);


		if (response.LetterData.length === 0) {
			statusElement.textContent = 'No letters found.';
			return;
		}

		statusElement.textContent = '';
		for (const item of response.LetterData) {
			resultsElement.innerHTML += `<div>${item.Letter}: ${item.LetterCount} (${item.LetterPercentage.toFixed(1)}%)</div>`;
		}
	} catch {
		statusElement.textContent = 'Error.';
	}
});