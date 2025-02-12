from flask import Flask, request, jsonify
from flask_cors import CORS

app = Flask(__name__)
CORS(app)  # Enable CORS for frontend communication

# Sample questions and answers
listening_exercises = [
    {"audio": "audio1.mp3", "question": "What is the main idea of the audio?", "answer": "Technology advancement"},
]

reading_exercises = [
    {"paragraph": "Artificial Intelligence is transforming industries worldwide.", 
     "question": "What is AI transforming?", "answer": "Industries"},
]

writing_exercises = [
    {"prompt": "Write a short essay about climate change.", "word_limit": 150}
]

@app.route('/get-listening', methods=['GET'])
def get_listening():
    return jsonify(listening_exercises)

@app.route('/get-reading', methods=['GET'])
def get_reading():
    return jsonify(reading_exercises)

@app.route('/submit-writing', methods=['POST'])
def submit_writing():
    data = request.json
    response = "Your submission has been received: " + data['text']
    return jsonify({"message": response})

if __name__ == '__main__':
    app.run(debug=True)
