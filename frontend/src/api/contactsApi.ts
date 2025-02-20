export type createMessageData = {
  name:string, email:string, text:string
}

export async function createMessage(data : createMessageData) {
  return fetch("/api/messages",
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    }
  )
}