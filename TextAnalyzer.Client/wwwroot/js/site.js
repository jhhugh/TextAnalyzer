const textInput = document.getElementById('text-input');
const submitBtn = document.getElementById('submit-btn');
const statusElement = document.getElementById('status');
const resultsElement = document.getElementById('results');
const resultsModal = new bootstrap.Modal(document.getElementById('results-modal'));


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
			resultsElement.innerHTML += `
				<div class="letter-row">
					<div class="fw-semibold text-center">${item.Letter}</div>
					<div class="text-secondary text-end small">${item.LetterCount}</div>
					<div class="progress" style="height: 1.25rem">
						<div class="progress-bar" style="width: ${item.LetterPercentage}%"></div>
					</div>
					<div class="text-end small">${item.LetterPercentage.toFixed(2)}%</div>
				</div>
			`;
		}

		textInput.value = '';
		submitBtn.disabled = true;

		resultsModal.show();
	} catch {
		statusElement.textContent = 'Error.';
	}
});