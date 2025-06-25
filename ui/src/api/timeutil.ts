/**
 * 获取指定时区的当前时间戳（秒）
 * @param timezone 时区偏移量（小时），默认使用本地时区
 * @returns 当前时间戳（秒）
 */
export function getCurrentTimestamp(timezone: number = null): number {
  if (timezone === null) {
    // 使用本地时区时间戳
    return Math.floor(Date.now() / 1000)
  } else {
    // 计算指定时区的时间戳
    const localTimestamp = Date.now()
    const localOffset = new Date().getTimezoneOffset() * 60 // 本地时区偏移（分钟转秒）
    const targetOffset = timezone * 3600 // 目标时区偏移（小时转秒）
    
    // 计算目标时区的时间戳（秒）
    return Math.floor((localTimestamp / 1000) + localOffset + targetOffset)
  }
}

/**
 * 获取当前时间并格式化为 YYYY-MM-DD HH:mm:ss 格式
 * @param timezone 时区偏移量（小时），默认使用本地时区
 * @returns 格式化后的时间字符串
 */
export function formatCurrentTime(timezone: number = null): string {
  return formatTimestamp(getCurrentTimestamp(timezone), timezone)
}

/**
 * 将时间戳转换为 YYYY-MM-DD HH:mm:ss 格式的字符串
 * @param timestamp 时间戳（秒）
 * @param timezone 时区偏移量（小时），默认使用本地时区
 * @returns 格式化后的时间字符串
 */
export function formatTimestamp(timestamp: number, timezone: number = null): string {
  // 将秒转换为毫秒
  const timestampMs = timestamp * 1000
  let date: Date
  
  if (timezone !== null) {
    const utcTime = new Date(timestampMs).getTime() + new Date().getTimezoneOffset() * 60000
    date = new Date(utcTime + timezone * 3600000)
  } else {
    date = new Date(timestampMs)
  }
  
  const padZero = (num: number): string => 
    num.toString().padStart(2, '0')
  
  return `${date.getFullYear()}-${padZero(date.getMonth() + 1)}-${padZero(date.getDate())} ` +
         `${padZero(date.getHours())}:${padZero(date.getMinutes())}:${padZero(date.getSeconds())}`
}

/**
 * 将 YYYY-MM-DD HH:mm:ss 格式的字符串转换为时间戳
 * @param timeString 时间字符串
 * @param timezone 时区偏移量（小时），默认使用本地时区
 * @returns 时间戳（秒），转换失败时返回当前时间戳
 */
export function stringToTimestamp(timeString: string, timezone: number = null): number {
  const normalized = timeString
    .replace(/-/g, '/')
    .replace(/T/, ' ')
    .replace(/\.\d{3}/, '')
    
  let timestamp: number
  
  if (timezone !== null) {
    const [datePart, timePart] = normalized.split(' ')
    const [year, month, day] = datePart.split('/').map(Number)
    const [hours = 0, minutes = 0, seconds = 0] = (timePart || '').split(':').map(Number)
    
    const utcDate = Date.UTC(
      year, 
      month - 1, 
      day, 
      hours - timezone,
      minutes, 
      seconds
    )
    
    timestamp = utcDate
  } else {
    timestamp = Date.parse(normalized)
  }
  
  // 将毫秒转换为秒
  return Math.floor(isNaN(timestamp) ? Date.now() / 1000 : timestamp / 1000)
}  

/**
 * 时间格式转换方法（原生 Date 实现）
 * @param time 原始时间字符串，如 "2025-06-18T10:15:07.5947633"
 * @param timezone 时区偏移（如 "+0800" 代表东八区，格式要符合 Date 构造函数要求 ），不传则用本地时区
 * @returns 格式化后的时间字符串，如 "2025-06-18 10:15:07"
 */
export const formatTimeNative = (
  time: string, 
  timezone?: string
): string => {
  let date: Date;
  if (timezone) {
    // 带时区的时间字符串拼接（比如 "2025-06-18T10:15:07.5947633+0800" ）
    const timeWithTimezone = `${time}${timezone}`; 
    date = new Date(timeWithTimezone);
  } else {
    date = new Date(time);
  }
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, '0'); // 月份从 0 开始，要 +1
  const day = date.getDate().toString().padStart(2, '0');
  const hours = date.getHours().toString().padStart(2, '0');
  const minutes = date.getMinutes().toString().padStart(2, '0');
  const seconds = date.getSeconds().toString().padStart(2, '0');
  return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
};